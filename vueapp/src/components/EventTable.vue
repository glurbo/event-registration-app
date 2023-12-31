<template lang="">
  <table class="table table-striped">
    <thead>
      <tr>
        <th>Event Name</th>
		<th>Event Date</th>
        <th>Participants</th>
        <th>Options</th>
      </tr>
    </thead>
    <tbody>
      <tr v-if="events.length === 0" class="align-middle">
        <td>No events found.</td>
      </tr>
      <tr v-else v-for="event in events" :key="event.Id">
        <td>{{event.Name}}</td>
		<td>{{getEventDate(event)}}</td>
        <td>{{event.Registrations.length}} / {{event.MaxParticipants}}
          <button v-if="eventHasRegistrations(event)" type="button" class="btn btn-light mr-1" @click="showRegistrationsDialog(event)" data-bs-toggle="modal" data-bs-target="#modalDialog">
			<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-person-fill" viewBox="0 0 16 16">
              <path d="M3 14s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1H3Zm5-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6Z"/>
            </svg>
          </button>
		</td>
        <td>
          <button v-if="canRegisterToEvent(event) && !isAuthenticated()" type="button" class="btn btn-light mr-1" @click="registrationDialog(event)" data-bs-toggle="modal" data-bs-target="#modalDialog">
			<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-person-plus-fill" viewBox="0 0 16 16">
              <path d="M1 14s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1H1zm5-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6z"/>
              <path fill-rule="evenodd" d="M13.5 5a.5.5 0 0 1 .5.5V7h1.5a.5.5 0 0 1 0 1H14v1.5a.5.5 0 0 1-1 0V8h-1.5a.5.5 0 0 1 0-1H13V5.5a.5.5 0 0 1 .5-.5z"/>
            </svg>
          </button>
          <button v-if="isAuthenticated()" type="button" class="btn btn-light mr-1" @click="editEventDialog(event)" data-bs-toggle="modal" data-bs-target="#modalDialog">
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16" >
              <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" />
              <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z" />
            </svg>
          </button>
          <button v-if="isAuthenticated()" type="button" class="btn btn-light mr-1" @click="deleteEventDialog(event)" data-bs-toggle="modal" data-bs-target="#modalDialog">
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash-fill" viewBox="0 0 16 16" >
              <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z" />
            </svg>
          </button>
        </td>
      </tr>
    </tbody>
  </table>
</template>

<script lang="ts">
import { EventModel } from './EventModels';
import { defineComponent, PropType } from 'vue';

export default defineComponent({
    props: {
        events: Array as PropType<EventModel[]>
    },
    methods: {
        isAuthenticated() {
            return sessionStorage.getItem("jwt") ? true : false;
        },
        editEventDialog(event: EventModel) {
            this.$emit("editEventDialog", event);
        },
        deleteEventDialog(event: EventModel) {
            this.$emit("deleteEventDialog", event);
        },
		registrationDialog(event: EventModel) {
			this.$emit("registrationDialog", event);
		},
		showRegistrationsDialog(event: EventModel) {
			this.$emit("showRegistrationsDialog", event);
		},
		canRegisterToEvent(event: EventModel): boolean {
			return event.Registrations.length < event.MaxParticipants;
		},
		eventHasRegistrations(event: EventModel): boolean {
            return event.Registrations.length >= 1;
		},
		getEventDate(event: EventModel) {
			let date = new Date(event.Date);
			if (isNaN(date.getTime())) {
				return ""; // Invalid date
			}
			return date.toLocaleString("et-EE", { year: 'numeric', month: 'numeric', day: 'numeric', hour: '2-digit', minute: '2-digit' });
		}
    },
})
</script>
<style lang=""></style>
