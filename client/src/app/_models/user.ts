export interface User {
    username: string;
    token: string;
    role: string;
    latitude?: number;
    longitude?: number;
    location?: string;
}