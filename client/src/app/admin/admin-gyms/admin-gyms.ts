import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { AdminService } from '../../_services/admin';
import { Gym } from '../../_models/gym';
import { FormsModule } from '@angular/forms';
import * as L from 'leaflet';
import { Pagination } from '../../shared/pagination/pagination';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-admin-gyms',
  imports: [FormsModule, Pagination],
  templateUrl: './admin-gyms.html',
  styleUrl: './admin-gyms.css',
})
export class AdminGyms implements OnInit {
  gyms: Gym[] = [];

  currentPage = 1;
  pageSize = 10;
  totalCount = 0;
  totalPages = 1;

  private map: L.Map | undefined;
  private marker: L.Marker | undefined;

  showForm = false;
  showEditForm = false;
  editingGym: Gym | null = null;
  isSaving = false;

  newGym: Gym = {
    id: 0,
    name: '',
    address: '',
    location: '',
    latitude: 0,
    longitude: 0,
    phone: '',
    openingHours: '',
    closingHours: '',
    description: '',
    imageUrl: '',
    socialMedia: ''
  };

  constructor(private adminService: AdminService, private changeDetector: ChangeDetectorRef, private http: HttpClient) { }

  ngOnInit(): void {
    this.loadGyms();
  }

  loadGyms() {
    this.adminService.getGyms(this.currentPage, this.pageSize).subscribe({
      next: result => {
        this.gyms = result.items;
        this.totalCount = result.totalCount;
        this.totalPages = Math.ceil(this.totalCount / this.pageSize);
        this.changeDetector.detectChanges();
      },
      error: error => {
        console.error('Error loading gyms:', error);
      }
    });
  }

  changePage(page: number) {
    this.currentPage = page;
    this.loadGyms();
  }

  showAddForm() {
    this.showForm = true;

    setTimeout(() => {
      this.initializeMap();
    });
  }

  addGym(): void {
    if (this.isSaving) {
      return;
    }

    this.isSaving = true;

    console.log('GYM BEING SENT TO API:', this.newGym);

    this.adminService.addGym(this.newGym).subscribe({
      next: gym => {
        console.log('GYM RETURNED FROM API:', gym);

        this.gyms.push(gym);
        this.showForm = false;
        this.resetNewGym();
        this.isSaving = false;

        if (this.map) {
          this.map.remove();
          this.map = undefined;
        }

        this.marker = undefined;

        this.changeDetector.detectChanges();
      },
      error: error => {
        console.error('Error adding gym:', error);
        this.isSaving = false;
      }
    });
  }
  cancelAdd() {
    this.showForm = false;
  }
  resetNewGym(): void {
    this.newGym = {
      id: 0,
      name: '',
      address: '',
      latitude: 0,
      longitude: 0,
      location: '',
      phone: '',
      openingHours: '',
      closingHours: '',
      description: '',
      imageUrl: '',
      socialMedia: ''
    };
  }

  editGym(gym: Gym) {
    this.editingGym = { ...gym };
    this.showEditForm = true;
  }

  saveEdit() {
    if (!this.editingGym) return;

    this.adminService.updateGym(this.editingGym.id, this.editingGym).subscribe({
      next: updatedGym => {
        const index = this.gyms.findIndex(gym => gym.id === updatedGym.id);
        if (index !== -1) this.gyms[index] = updatedGym;
        this.showEditForm = false;
        this.editingGym = null;
        this.changeDetector.detectChanges();
      },
      error: error => {
        console.error('Error updating gym:', error);
      }
    });
  }

  cancelEdit() {
    this.showEditForm = false;
    this.editingGym = null;
  }

  deleteGym(id: number) {
    if (!confirm('Are you sure you want to delete this gym?')) return;

    this.adminService.deleteGym(id).subscribe({
      next: () => {
        this.gyms = this.gyms.filter(gym => gym.id !== id);
        this.changeDetector.detectChanges();
      },
      error: error => {
        console.error('Error deleting gym:', error);
      }
    });
  }

  initializeMap() {
    this.map = L.map('map').setView([33.8938, 35.5018], 13);

    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: '&copy; OpenStreetMap contributors'
    }).addTo(this.map);
    
    const gymIcon = L.divIcon({
      html: '📍',
      className: 'gym-marker',
      iconSize: [30, 30],
      iconAnchor: [15, 30],
      popupAnchor: [0, -30]
    });

    this.map.on('click', (event: L.LeafletMouseEvent) => {

      const latitude = event.latlng.lat;
      const longitude = event.latlng.lng;
      this.newGym.latitude = latitude;
      this.newGym.longitude = longitude;

      this.getLocationName(latitude, longitude);
      if (this.marker) {
        this.marker.remove();
      }
      this.marker = L.marker(
        [latitude, longitude],
        {
          icon: gymIcon
        }
      ).addTo(this.map!);

      this.marker
        .bindPopup('Gym location')
        .openPopup();

      this.changeDetector.detectChanges();
    });
    setTimeout(() => {
      this.map?.invalidateSize();
    }, 200);
  }
  getLocationName(latitude: number, longitude: number): void {
    const url = 'https://nominatim.openstreetmap.org/reverse';

    this.http.get<any>(url, {
      params: {
        lat: latitude,
        lon: longitude,
        format: 'jsonv2',
        addressdetails: 1,
        zoom: 18,
        'accept-language': 'en'
      }
    }).subscribe({
      next: result => {
        console.log('LOCATION RESULT:', result);

        this.newGym.location = result.display_name;

        this.changeDetector.detectChanges();
      },

      error: error => {
        console.error('Error getting location:', error);

        this.newGym.location =
          `${latitude.toFixed(6)}, ${longitude.toFixed(6)}`;

        this.changeDetector.detectChanges();
      }
    });
  }
}