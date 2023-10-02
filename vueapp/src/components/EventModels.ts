export interface EventData {
	Id: string,
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

export enum EDialogType {
	AddEvent,
	UpdateEvent,
	DeleteEvent,
	Registration
}