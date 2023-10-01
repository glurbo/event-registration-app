export interface EventData {
	id: string,
	name: string, 
	date: Date,
	maxParticipants: number,
	registrations: Registration[]
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
	DeleteEvent
}