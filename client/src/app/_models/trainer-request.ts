export interface TrainerRequest
{
    id?: number;
    userId?: number;
    specialization: string;
    description?: string;
    yearsOfExperience?: number;
    gymId?: number;
    otherGymName?: string;
    worksIndependently: boolean;
    status?: string;
}