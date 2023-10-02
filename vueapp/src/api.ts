import axios from 'axios';
import { EventData, Registration } from './components/EventModels';

const apiClient = axios.create({
    baseURL: 'https://localhost:44391/api/',
});

const axiosConfig = {
    headers: {
        'Content-Type': 'application/json',
    },
};

export const fetchEvents = async () => await apiClient.get('Event');
export const fetchEventDetails = async (eventId: string) => await apiClient.get(`Event/${eventId}`);
export const fetchRegistrations = async (eventId: string) => await apiClient.get(`Registration/${eventId}`);
export const registerToAnEvent = async (eventId: string, registration: Registration) => await apiClient.post(`Registration`, { eventId: eventId, registration: registration }, axiosConfig);
export const createEvent = async (eventData: EventData) => await apiClient.post(`Event`, { eventData}, axiosConfig);
export const deleteEvent = async (eventId: string) => await apiClient.delete(`Event/${eventId}`);
export const updateEvent = async (eventId: string, event: EventData) => await apiClient.put(`Event/${eventId}`, event, axiosConfig);