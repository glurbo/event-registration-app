<template>
  <div>
    <h2>Events</h2>
	<div v-if="!loading">
		<button type="button"
            class="btn btn-primary m-2 fload-end"
            @click="addEvent()">
            Add Event
        </button>
		<table class="table table-striped">
		<thead>
			<tr>
				<th>
					Event Id
				</th>
				<th>
					Event Name
				</th>
				<th>
					Participants
				</th>
				<th>
					Options
				</th>
			</tr>
		</thead>
		<tbody>
			<tr v-if="events.length === 0" class="align-middle">
				<td>
					No events found.
				</td>
			</tr>
			<tr v-else v-for="event in events" :key="event.Id">
				<td>{{event.id}}</td>
				<td>{{event.name}}</td>
				<td>{{event.registrations.length}} / {{event.maxParticipants}}</td>
				<td>
					<button type="button" class="btn btn-light mr-1" @click="showEventDetails(event.id)">
						<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
						<path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
						<path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"/>
						</svg>
					</button>
					<button type="button" class="btn btn-light mr-1" @click="deleteEvent(event.id)">
						<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash-fill" viewBox="0 0 16 16">
						<path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z"/>
						</svg>
					</button>
				</td>
			</tr>
		</tbody>
		</table>
	</div>
  </div>
</template>

<script lang="ts">
import {defineComponent} from 'vue'
import { fetchEvents } from '@/api';

export interface Event {
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

export default defineComponent({
  name: "EventList",
  data() {
    return {
      events: [] as Event[],
      loading: false
    };
  },
  mounted() {
    this.fetchEvents();
  },
  methods: {
    fetchEvents() {
      fetchEvents()
          .then((response) => {
            this.events = response.data;
			this.loading = false;
			console.log(this.events)
          })
          .catch((error) => {
            console.error('Error fetching events:', error);
          });
    },
    showEventDetails(eventId: string) {
		console.log("show event details", eventId)
      // Implement navigation to event details page
    },
	deleteEvent(eventId: string) {
		console.log("delete event", eventId)
	},
	addEvent() {
		console.log("add event")
	}
  },
});
</script>

<style scoped>

</style>