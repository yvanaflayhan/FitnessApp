import { ChangeDetectorRef, Component } from '@angular/core';
import { Account } from '../_services/account';
import { TitleCasePipe } from '@angular/common';
import { UserService } from '../_services/User';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-profile',
  imports: [TitleCasePipe, FormsModule],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class Profile {
  user: any;
  showUsernameForm = false;
  newUsername = '';

  constructor(public account: Account, private userService: UserService, private http: HttpClient, private changeDetector: ChangeDetectorRef) {
    this.account.currentUser$.subscribe(user => {
      this.user = user;
    });
  }

  getLocation(): void {

    if (!navigator.geolocation) {
      alert('Geolocation is not supported by your browser.');
      return;
    }

    navigator.geolocation.getCurrentPosition(

      position => {

        const latitude = position.coords.latitude;
        const longitude = position.coords.longitude;

        console.log('Latitude:', latitude);
        console.log('Longitude:', longitude);

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

            const location = result.display_name;

            console.log('Readable location:', location);

            this.userService.updateCurrentUserLocation(
              latitude,
              longitude,
              location
            ).subscribe({

              next: user => {

                console.log('Location updated:', user);

                this.user = user;

                this.changeDetector.detectChanges();

                alert('Your location has been updated successfully!');
              },

              error: error => {

                console.error('Error saving location:', error);

                alert('Could not save your location.');
              }
            });
          },

          error: error => {

            console.error('Error getting location:', error);

            // Still save coordinates if reverse geocoding fails
            this.userService.updateCurrentUserLocation(
              latitude,
              longitude,
              `${latitude.toFixed(6)}, ${longitude.toFixed(6)}`
            ).subscribe({

              next: user => {

                this.user = user;

                this.changeDetector.detectChanges();

                alert('Your coordinates were saved, but the address could not be determined.');
              },

              error: saveError => {
                console.error('Error saving location:', saveError);
                alert('Could not save your location.');
              }
            });
          }
        });
      },

      error => {

        console.error('Location error:', error);

        if (error.code === 1) {
          alert('Please allow location access in your browser.');
        }
        else if (error.code === 2) {
          alert('Your location could not be determined.');
        }
        else if (error.code === 3) {
          alert('Location request timed out.');
        }
      }
    );
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

        const locationName = result.display_name;

        console.log('Readable location:', locationName);

        this.changeDetector.detectChanges();
      },

      error: error => {
        console.error('Error getting location:', error);
      }
    });
  }

  updateUsername(): void {
    if (!this.newUsername.trim()) {
      alert('Please enter a username.');
      return;
    }

    this.userService.updateCurrentUsername(this.newUsername.trim())
      .subscribe({
        next: user => {
          console.log('Username updated:', user);

          this.user = user;

          this.showUsernameForm = false;
          this.newUsername = '';

          this.changeDetector.detectChanges();

          alert('Username updated successfully!');
        },

        error: error => {
          console.error('Error updating username:', error);
          alert('Could not update username.');
        }
      });
  }
}
