<template>
  <div class="container">
    <h2>Events</h2>
	<div v-if="!loading">
		<button type="button"
            class="btn btn-primary m-2 fload-end"
            @click="addEventDialog" data-bs-toggle="modal" data-bs-target="#modalDialog">
            Add Event
        </button>
		<event-table
			:key="componentKey"
			:events="events"
			@delete-event-dialog="deleteEventDialog"
			@edit-event-dialog="editEventDialog"
			@registration-dialog="registrationDialog"
			@show-registrations-dialog="showRegistrationsDialog">
		</event-table>
		<event-dialog 
			:event="selectedEvent"
			:dialog-type="dialogType"
			@cancel-clicked="cancelClicked"
			@save-clicked="saveClicked"
			@update-clicked="updateClicked"
			@delete-clicked="deleteClicked"
			@register-clicked="registerToEvent">
		</event-dialog>
	</div>
  </div>
</template>

<script lang="ts">
import {defineComponent} from 'vue'
import { fetchEvents, createEvent, deleteEvent, updateEvent, registerToAnEvent } from '@/api';
import EventDialog from './EventDialog.vue';
import EventTable from './EventTable.vue';
import { EDialogType, EventModel, RegisterFormModel } from './EventModels';

export default defineComponent({
  components: { EventDialog, EventTable },
  name: "EventList",
  data() {
    return {
      events: [] as EventModel[],
      loading: false,
      dialogType: null as EDialogType | null,
      selectedEvent: null as EventModel | null,
      componentKey: 0
    };
  },
  mounted() {
    this.fetchEvents();
  },
  methods: {
    fetchEvents() {
        this.loading = true;
		this.componentKey += 1;
        fetchEvents()
			.then(response => response.data)
            .then((response: EventModel[]) => {
                this.events = response;
                this.loading = false;
            })
            .catch((error) => {
                console.error('Error fetching events:', error);
            });
		this.componentKey += 1; // rerender table
		this.loading = false;
    },
    editEventDialog(event: EventModel) {
		this.dialogType = EDialogType.UpdateEvent;
		this.selectedEvent = event;
    },
	deleteEventDialog(event: EventModel) {
		this.dialogType = EDialogType.DeleteEvent;
		this.selectedEvent = event;
	},
	addEventDialog() {
		this.dialogType = EDialogType.AddEvent;
		this.selectedEvent = null;
	},
	registrationDialog(event: EventModel) {
		this.dialogType = EDialogType.Registration;
		this.selectedEvent = event;
	},
	showRegistrationsDialog(event: EventModel) {
		this.dialogType = EDialogType.ShowRegistrations;
		this.selectedEvent = event;
	},
	cancelClicked() {
		this.selectedEvent = null;
		this.dialogType = null;
	},
	saveClicked(event: EventModel) {
		createEvent(event)
			.then(() => this.fetchEvents())
			.catch((error) => {console.error("error creating event", error)});
	},
	updateClicked(id: string, event: EventModel) {
		updateEvent(id, event).then(() => this.fetchEvents());
	},
	deleteClicked(eventId: string) {
		deleteEvent(eventId).then(() => this.fetchEvents());
	},
	registerToEvent(eventId: string, registration: RegisterFormModel) {
		registerToAnEvent(eventId, registration).then(() => this.fetchEvents());
	}
  },
});
</script>

<style scoped>

</style>