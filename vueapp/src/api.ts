import axios from 'axios';
import { EventModel, RegisterFormModel } from './components/EventModels';

const apiClient = axios.create({
    baseURL: 'https://localhost:44391/api/',
});

const axiosConfig = {
    headers: {
        'Content-Type': 'application/json',
    },
};


export const fetchEvents = async () => await apiClient.get('Event');
export const registerToAnEvent = async (eventId: string, registration: RegisterFormModel) => await apiClient.post(`Registration?eventId=${eventId}`, registration, axiosConfig)
export const createEvent = async (eventData: EventModel) => await apiClient.post(`Event`, eventData);
export const deleteEvent = async (eventId: string) => await apiClient.delete(`Event/${eventId}`);
export const updateEvent = async (eventId: string, event: EventModel) => await apiClient.put(`Event/${eventId}`, event, axiosConfig);