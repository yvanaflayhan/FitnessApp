export interface TrainerRequest {
    id?: number;
    userId?: number;
    user?: {
        id: number;
        userName: string;
        fullName: string;

    };
    specialization: string;
    description?: string;
    yearsOfExperience?: number;
    gymId?: number;
    gym?: {
        id: number;
        name: string;
        address: string;
    };
    otherGymName?: string;
    worksIndependently: boolean;
    status?: string;
}