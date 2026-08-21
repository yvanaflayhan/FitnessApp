export interface Trainer{
    id: number;
    userId: number;
    specialization: string;
    description?: string;
    yearsOfExperience?: number;
    phone?: string;
    imageUrl?: string;
    isApproved: boolean;
}