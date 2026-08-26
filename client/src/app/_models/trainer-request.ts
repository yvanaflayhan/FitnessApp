export interface TrainerRequest {
    id?: number;
    userId?: number;
    user?: {
        id: number;
        userName: string;
        fullName: string;

    };
    age?: number;
    gender?: string;
    phone?: string;
    height?: number;
    weight?: number;
    imageUrl?: string;
    specialization: string;
    description?: string;
    yearsOfExperience?: number;
    skills?: string;
    gymId?: number;
    gym?: {
        id: number;
        name: string;
        address: string;
    };
    otherGymName?: string;
    worksIndependently: boolean;
    cvUrl?: string;
    status?: string;
}