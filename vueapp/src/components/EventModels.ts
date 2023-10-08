export interface EventModel {
	Id: string | null,
	Name: string, 
	Date: Date,
	MaxParticipants: number,
	Registrations: Registration[]
}

export interface Registration {
	Id: string,
	FirstName: string,
	LastName: string,
	IdentificationCode: string
}

export interface EventFormModel {
	Name: string, 
	Date: Date,
	MaxParticipants: number,
}

export interface RegisterFormModel {
	FirstName: string,
	LastName: string,
	IdentificationCode: string
}

export interface LoginModel {
	Email: string,
	Password: string
}

export enum EDialogType {
	AddEvent,
	UpdateEvent,
	DeleteEvent,
	Registration,
	ShowRegistrations
}