import axios from 'axios';

const apiClient = axios.create({
    baseURL: 'https://localhost:44391/api/',
});

export const fetchEvents = async () => await apiClient.get('Event');
export const fetchEventDetails = async (eventId: string) => await apiClient.get(`Event/${eventId}`);
export const fetchRegistrations = async (eventId: string) => await apiClient.get(`Registration/${eventId}`);
// export const registerToAnEvent = async (eventId: string) => await apiClient.post(`Registration/${eventId}`)
