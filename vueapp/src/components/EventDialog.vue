<template lang="">
	<div class="modal fade" id="modalDialog" tabindex="-1" role="dialog" aria-labelledby="modalDialogLabel" aria-hidden="true">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
			<div class="modal-header">
				<h5 class="modal-title" id="modalDialogLabel">{{dialogTitle}}</h5>
				<button type="button" class="close" data-dismiss="modal" aria-label="Close">
				<span aria-hidden="true">&times;</span>
				</button>
			</div>
			<div class="modal-body">
				<div v-if="isAddOrUpdateDialog">
					
				</div>
				<div v-else>
					<tr>
						{{eventId}}
					</tr>
					<tr>
						<td>{{eventName}}</td>
					</tr>
					<tr>
						<td>{{eventDate}}</td>
					</tr>
					<tr>
						<td>{{eventParticipationCount}}</td>
					</tr>
				</div>
				
			</div>
			<div class="modal-footer">
				<button type="button" class="btn btn-secondary" data-dismiss="modal" @click="$emit('cancelClicked')">Close</button>
				<button type="button" class="btn btn-primary" @click="onClickConfirm()">{{confirmButtonText}}</button>
			</div>
			</div>
		</div>
	</div>
</template>
<script lang="ts">
import { EDialogType } from './EventModels'

export default {
	props: {
		event: Object,
		dialogTitle: String,
		dialogType: EDialogType
	},
	computed: {
		isAddOrUpdateDialog() {
			return this.dialogType === EDialogType.AddEvent || this.dialogType === EDialogType.UpdateEvent;
		},
		eventId() {
			return this.event != null ? this.event.id : "";
		},
		eventName() {
			return this.event != null ? this.event.name : "";
		},
		eventDate() {
			if (this.event == null) return "";
			let date = new Date(this.event.date);
			console.log(date)
			return date.toISOString().substr(0, 16).replace('T', ' ');
		},
		eventParticipationCount() {
			return this.event != null ? `${this.event.registrations.length} / ${this.event.maxParticipants}` : "";
		},
		confirmButtonText() {
			switch (this.dialogType) {
				case EDialogType.AddEvent:
					return "Save";
				case EDialogType.UpdateEvent:
					return "Update";
				case EDialogType.DeleteEvent:
					return "Confirm";
				default:
					return ""
			}
		}
	},
	methods: {
		onClickConfirm() {
			switch (this.dialogType) {
				case EDialogType.AddEvent:
					this.$emit('saveClicked');
					break;
				case EDialogType.UpdateEvent:
					this.$emit('updateClicked');
					break;
				case EDialogType.DeleteEvent:
					this.$emit('deleteClicked');
					break;
				default:
					break;
			}
		}
	},
};
</script>
<style lang="ts">
	
</style>