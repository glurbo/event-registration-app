import axios from 'axios';
import { EventModel, RegisterFormModel, LoginModel } from './components/EventModels';

const apiClient = axios.create({
    baseURL: 'https://localhost:44391/api/',
	headers: {
		'Content-Type': 'application/json',
	}
});

function getHeaders() {
	const token = sessionStorage.getItem("jwt");
	if (!token) return;
	return { 
		headers: {
			'Authorization': `Bearer ${token}`
		}
	}
}

export const fetchEvents = async () => await apiClient.get('Event');
export const registerToAnEvent = async (eventId: string, registration: RegisterFormModel) => await apiClient.post(`Registration?eventId=${eventId}`, registration)
export const createEvent = async (eventData: EventModel) => await apiClient.post(`Event`, eventData, getHeaders());
export const deleteEvent = async (eventId: string) => await apiClient.delete(`Event/${eventId}`, getHeaders());
export const updateEvent = async (eventId: string, event: EventModel) => await apiClient.put(`Event/${eventId}`, event, getHeaders());

export const LogIn = async (loginData: LoginModel) => await apiClient.post('identity/Account/Login', loginData, getHeaders()).then(response => {
	sessionStorage.setItem("jwt", response.data);
	sessionStorage.setItem("username", loginData.Email);
});
export const LogOut = async () => await apiClient.post('identity/Account/Logout', null, getHeaders()).then(() => {
	sessionStorage.removeItem("jwt"); 
	sessionStorage.removeItem("username");
});