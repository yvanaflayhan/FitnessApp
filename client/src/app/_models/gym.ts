export interface Gym {
    id: number;
    name: string;
    address: string;
    location: string;
    latitude: number;
    longitude: number;
    phone?: string;
    openingHours?: string;
    closingHours?: string;
    description?: string;
    imageUrl?: string;
    socialMedia?: string;
}