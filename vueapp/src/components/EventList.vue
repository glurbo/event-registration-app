<template>
  <div>
    <h2>Events</h2>
	<div v-if="!loading">
		<button type="button"
            class="btn btn-primary m-2 fload-end"
            @click="addEventDialog" data-bs-toggle="modal" data-bs-target="#modalDialog">
            Add Event
        </button>
		<event-table
			:key="events"
			:events="events"
			@delete-event-dialog="deleteEventDialog"
			@edit-event-dialog="editEventDialog"
			@registration-dialog="registrationDialog">
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
import { EDialogType, EventData, Registration } from './EventModels';

export default defineComponent({
  components: { EventDialog, EventTable },
  name: "EventList",
  data() {
    return {
      events: [] as EventData[],
      loading: false,
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
    editEventDialog(event: EventData) {
		this.dialogType = EDialogType.UpdateEvent;
		this.selectedEvent = event;
    },
	deleteEventDialog(event: EventData) {
		this.dialogType = EDialogType.DeleteEvent;
		this.selectedEvent = event;
	},
	addEventDialog() {
		this.dialogType = EDialogType.AddEvent;
		this.selectedEvent = null;
	},
	registrationDialog(event: EventData) {
		this.dialogType = EDialogType.Registration;
		this.selectedEvent = event;
	},
	cancelClicked() {
		this.selectedEvent = null;
	},
	saveClicked(event: EventData) {
		console.log(event)
		createEvent(event);
		fetchEvents();
	},
	updateClicked(id: string, event: EventData) {
		updateEvent(id, event);
		fetchEvents();
	},
	deleteClicked(eventId: string) {
		deleteEvent(eventId);
        fetchEvents();
	},
	registerToEvent(eventId: string, registration: Registration) {
		registerToAnEvent(eventId, registration);
		fetchEvents();
	}
  },
});
</script>

<style scoped>

</style>