<template lang="">
	<div class="modal" id="modalDialog" tabindex="-1" role="dialog" data-bs-backdrop="static" aria-labelledby="modalDialogLabel" aria-hidden="true">
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
						<input v-model="editableEvent.Name" type="text" class="form-control" id="eventName" placeholder="Enter name" >
					</div>
					<div class="form-group">
						<label for="eventDate">Event date</label>
						<input v-model="editableEvent.Date" type="datetime-local" class="form-control" id="eventDate">
					</div>
					<div v-if="isAddDialog" class="form-outline">
						<label for="eventCount">Max participants</label>
						<input v-model="editableEvent.MaxParticipants" type="number" class="form-control" id="eventCount" min="1" max="5000">
					</div>
				</div>
				<div v-if="isDeleteDialog">
					<table class="table table-striped">
                    <tbody>
                        <tr>
                            <th>Event name:</th>
                            <th>{{eventName}}</th>
                        </tr>
                        <tr>
                            <th>Date:</th>
                            <th>{{eventDate}}</th>
                        </tr>
                        <tr>
                            <th>Participants:</th>
                            <th>{{eventParticipationCount}}</th>
                        </tr>
					</tbody>
					</table>
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
				<div v-if="isShowRegistrationsDialog">
					<table class="table table-striped">
                        <thead>
                            <tr>
                                <th>First name</th>
                                <th>Last name</th>
                                <th>Identification code</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="registration in event.Registrations" :key="registration.IdentificationCode">
                                <td>{{registration.FirstName}}</td>
                                <td>{{registration.LastName}}</td>
                                <td>{{registration.IdentificationCode}}</td>
                            </tr>
                        </tbody>
                    </table>
				</div>
			</div>
			<div class="modal-footer">
				<button type="button" class="btn btn-secondary" data-bs-dismiss="modal" @click="cancelClicked()">Close</button>
				<button type="button" class="btn btn-primary" data-bs-dismiss="modal" @click="onClickConfirm()" v-if="!isShowRegistrationsDialog" :disabled="!formIsValid">{{confirmButtonText}}</button>
			</div>
			</div>
		</div>
	</div>
</template>
<script lang="ts">
import { EDialogType, EventModel, RegisterFormModel } from './EventModels'
import { defineComponent } from 'vue';

export default defineComponent({
	props: ['event', 'dialogType'],
	data() {
		return {
			dialogTitle: "",
			editableEvent: {
				Id: null,
				Name: "",
				Date: new Date(),
				MaxParticipants: 0,
				Registrations: []
			} as EventModel,
			registrationData: {
				FirstName: "",
				LastName: "",
				IdentificationCode: ""
			} as RegisterFormModel,
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
		isShowRegistrationsDialog(): boolean {
            return this.dialogType === EDialogType.ShowRegistrations;
		},
		eventId(): string {
			return this.event != null ? this.event.Id : "";
		},
		eventName(): string {
			return this.event != null ? this.event.Name : "";
		},
		eventDate(): string {
			if (this.event == null || !this.event.Date) return "";
			let date = new Date(this.event.Date);
			if (isNaN(date.getTime())) {
				return ""; // Invalid date
			}
			return date.toLocaleString("et-EE", { year: 'numeric', month: 'numeric', day: 'numeric', hour: '2-digit', minute: '2-digit' });
		},
		eventParticipationCount(): string {
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
					return "Delete event?";
				case EDialogType.Registration:
					return "Register";
				case EDialogType.ShowRegistrations:
					return `Registrations for ${this.event.Name}`
				default:
					return "";
			}
		},
		formIsValid(): boolean {
			switch (this.dialogType) {
                case EDialogType.AddEvent:
				case EDialogType.UpdateEvent:
					return this.editableEvent.Name.length !== 0 && this.editableEvent.Date !== null && this.editableEvent.MaxParticipants > 0;
				case EDialogType.Registration:
					return this.registrationData.FirstName.length !== 0 && this.registrationData.LastName.length !== 0 && this.registrationData.IdentificationCode.length !== 0;
				default:
					return true;
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
			this.resetData();
		},
		cancelClicked() {
			this.$emit('cancelClicked');
			this.resetData();
		},
		resetData() {
			this.editableEvent = {
				Id: null,
				Name: "",
				Date: new Date(),
				MaxParticipants: 0,
				Registrations: []
			} as EventModel;
			this.registrationData = {
				FirstName: "",
				LastName: "",
				IdentificationCode: ""
			} as RegisterFormModel;
		}
	},
	watch: {
		event: {
			handler(newValue) {
				if (this.dialogType === EDialogType.UpdateEvent) {
                    this.editableEvent = structuredClone(newValue);
				}
			},
			deep: true
		}
	}
});
</script>
<style>

</style>