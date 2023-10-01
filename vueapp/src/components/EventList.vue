<template>
  <div>
    <h2>Events</h2>
	<div v-if="!loading">
		<button type="button"
            class="btn btn-primary m-2 fload-end"
            @click="addEvent()" data-bs-toggle="modal" data-bs-target="#modalDialog">
            Add Event
        </button>
		<table class="table table-striped">
		<thead>
			<tr>
				<th>Event Id</th>
				<th>Event Name</th>
				<th>Participants</th>
				<th>Options</th>
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
					<button type="button" class="btn btn-light mr-1" @click="editEvent(event)" data-bs-toggle="modal" data-bs-target="#modalDialog">
						<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
						<path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
						<path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"/>
						</svg>
					</button>
					<button type="button" class="btn btn-light mr-1" @click="deleteEvent(event)" data-bs-toggle="modal" data-bs-target="#modalDialog">
						<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash-fill" viewBox="0 0 16 16">
						<path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z"/>
						</svg>
					</button>
				</td>
			</tr>
		</tbody>
		</table>
		<event-dialog 
			:event="selectedEvent"
			:dialog-title="dialogTitle"
			:dialog-type="dialogType"
			@cancel-clicked="cancelClicked()"
			@save-clicked="saveClicked()"
			@update-clicked="updateClicked()"
			@delete-clicked="deleteClicked()">
		</event-dialog>
	</div>
  </div>
</template>

<script lang="ts">
import {defineComponent} from 'vue'
import { fetchEvents } from '@/api';
import EventDialog from './EventDialog.vue';
import { EDialogType, EventData } from './EventModels';

export default defineComponent({
    components: { EventDialog },
  name: "EventList",
  data() {
    return {
      events: [] as EventData[],
      loading: false,
      dialogTitle: "",
      dialogType: null as EDialogType | null,
      selectedEvent: null as EventData | null,
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
          })
          .catch((error) => {
            console.error('Error fetching events:', error);
          });
    },
    editEvent(event: EventData) {
		this.dialogTitle = "Edit event";
		this.dialogType = EDialogType.UpdateEvent;
		this.selectedEvent = event;
    },
	deleteEvent(event: EventData) {
		this.dialogTitle = "Are you sure you want to delete this event?";
		this.dialogType = EDialogType.DeleteEvent;
		this.selectedEvent = event;
		console.log("delete event", event)
	},
	addEvent() {
		this.dialogTitle = "Add new event";
		this.dialogType = EDialogType.AddEvent;
		this.selectedEvent = null;
	},
	cancelClicked() {
		this.dialogTitle = "";
		this.selectedEvent = null;
	},
	saveClicked() {
		console.log("saveClicked")
	},
	updateClicked() {
		console.log("updateClicked")
	},
	deleteClicked() {
		console.log("deleteClicked")
	}
  },
});
</script>

<style scoped>

</style>