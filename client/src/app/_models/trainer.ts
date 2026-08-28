export interface Trainer {
    id: number;
    userId: number;
    fullName: string;
    specialization: string;
    description?: string;
    yearsOfExperience?: number;
    skills?: string;
    phone?: string;
    imageUrl?: string;
    isApproved: boolean;
}