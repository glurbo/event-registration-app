<template lang="">
	<div class="modal fade" id="modalDialog" tabindex="-1" role="dialog" aria-labelledby="modalDialogLabel" aria-hidden="true">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
			<div class="modal-header">
				<h5 class="modal-title" id="modalDialogLabel">{{getDialogTitle}}</h5>
				<button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
				<span aria-hidden="true">&times;</span>
				</button>
			</div>
			<div class="modal-body">
				<div v-if="isAddDialog || isUpdateDialog">
					<div class="form-group">
						<label for="eventName">Event name</label>
						<input v-model="editableEvent.Name" type="text" class="form-control" id="eventName" placeholder="Enter name">
					</div>
					<div class="form-group">
						<label for="eventDate">Event date</label>
						<input v-model="editableEvent.Date" type="datetime-local" class="form-control" id="eventDate">
					</div>
					<div class="form-outline">
						<label for="eventCount">Max participants</label>
						<input v-model="editableEvent.MaxParticipants" type="number" class="form-control" id="eventCount">
					</div>
				</div>
				<div v-if="isDeleteDialog">
					<tr>{{eventId}}</tr>
					<tr>{{eventName}}</tr>
					<tr>{{eventDate}}</tr>
					<tr>{{eventParticipationCount}}</tr>
				</div>
				<div v-if="isRegistrationDialog">
					<div class="form-group">
						<label for="firstName">First name</label>
						<input v-model="registrationData.FirstName" type="text" class="form-control" id="firstName" placeholder="Enter name">
					</div>
					<div class="form-group">
						<label for="lastName">Last name</label>
						<input v-model="registrationData.LastName" type="text" class="form-control" id="lastName">
					</div>
					<div class="form-outline">
						<label for="idCode">Identification code</label>
						<input v-model="registrationData.IdentificationCode" type="text" class="form-control" id="idCode">
					</div>
				</div>
				
			</div>
			<div class="modal-footer">
				<button type="button" class="btn btn-secondary" data-bs-dismiss="modal" @click="$emit('cancelClicked')">Close</button>
				<button type="button" class="btn btn-primary" data-bs-dismiss="modal" @click="onClickConfirm()">{{confirmButtonText}}</button>
			</div>
			</div>
		</div>
	</div>
</template>
<script lang="ts">
import { EDialogType, EventData, Registration } from './EventModels'
import { PropType, defineComponent } from 'vue';

export default defineComponent({
	props: {
		event: Object as PropType<EventData>,
		dialogType: EDialogType
	},
	data() {
		return {
			dialogTitle: "",
			editableEvent: {
				Id: null,
				Name: null,
				Date: null,
				MaxParticipants: null,
				Registrations: []
			} as EventData,
			registrationData: {
				Id: null,
				FirstName: "",
				LastName: "",
				IdentificationCode: ""
			} as Registration,
		};
	},
	computed: {
		isAddDialog(): boolean {
			return this.dialogType === EDialogType.AddEvent;
		},
		isUpdateDialog(): boolean {
			return this.dialogType === EDialogType.UpdateEvent;
		},
		isRegistrationDialog(): boolean {
			return this.dialogType === EDialogType.Registration;
		},
		isDeleteDialog(): boolean {
			return this.dialogType === EDialogType.DeleteEvent;
		},
		eventId(): string {
			return this.event != null ? this.event.Id : "";
		},
		eventName(): string {
			return this.event != null ? this.event.Name : "";
		},
		eventDate(): string {
			if (this.event == null) return "";
			let date = new Date(this.event.Date);
			return date.toISOString().substr(0, 16).replace('T', ' ');
		},
		eventParticipationCount(): number {
			return this.event != null ? `${this.event.Registrations.length} / ${this.event.MaxParticipants}` : "";
		},
		confirmButtonText(): string {
			switch (this.dialogType) {
				case EDialogType.AddEvent:
					return "Save";
				case EDialogType.UpdateEvent:
					return "Update";
				case EDialogType.DeleteEvent:
					return "Confirm";
				case EDialogType.Registration:
					return "Register";
				default:
					return ""
			}
		},
		getDialogTitle(): string {
			switch (this.dialogType) {
				case EDialogType.AddEvent:
					return "Add new event";
				case EDialogType.UpdateEvent:
					return "Edit event";
				case EDialogType.DeleteEvent:
					return "Delete event";
				default:
					return "";
			}
		}
	},
	methods: {
		onClickConfirm(): void {
			switch (this.dialogType) {
				case EDialogType.AddEvent:
					this.$emit('saveClicked', this.editableEvent);
					break;
				case EDialogType.UpdateEvent:
					this.$emit('updateClicked', this.editableEvent.Id, this.editableEvent);
					break;
				case EDialogType.DeleteEvent:
					this.$emit('deleteClicked', this.eventId);
					break;
				case EDialogType.Registration:
					this.$emit('registerClicked', this.eventId, this.registrationData);
					break;
				default:
					break;
			}
		}
	},
	watch: {
		event(newEvent: EventData, oldEvent: EventData) {
			if (this.dialogType == EDialogType.UpdateEvent && oldEvent === null) {
				this.editableEvent = newEvent;
			} else {
				this.editableEvent.Id = null;
				this.editableEvent.Name = "";
				this.editableEvent.Date = null;
                this.editableEvent.MaxParticipants = null;
				this.editableEvent.Registrations = [];
			}
		}
	}
});
</script>
<style>

</style>