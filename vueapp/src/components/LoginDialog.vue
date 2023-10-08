<template lang="">
	<div class="modal" id="loginDialog" tabindex="-1" role="dialog" data-bs-backdrop="static" aria-labelledby="loginDialogLabel" aria-hidden="true">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
			<div class="modal-header">
				<h5 class="modal-title" id="loginDialogLabel">Log in</h5>
				<button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
				<span aria-hidden="true">&times;</span>
				</button>
			</div>
			<div class="modal-body">
				<div class="form-group">
					<label for="email">Email</label>
					<input v-model="loginData.Email" type="text" class="form-control" id="email" placeholder="Enter email" >
				</div>
				<div class="form-group">
					<label for="password">Password</label>
					<input v-model="loginData.Password" type="password" class="form-control" id="password">
				</div>
			</div>
			<div class="modal-footer">
				<button type="button" class="btn btn-secondary" data-bs-dismiss="modal" @click="cancelClicked()">Close</button>
				<button type="button" class="btn btn-primary" data-bs-dismiss="modal" @click="onClickConfirm()" v-if="!isShowRegistrationsDialog">Log in</button>
			</div>
			</div>
		</div>
	</div>
</template>
<script lang="ts">
import { LoginModel } from './EventModels'
import { defineComponent } from 'vue';

export default defineComponent({
	data() {
		return {
			loginData: {
				Email: "",
				Password: "",
			} as LoginModel,
		};
	},
	computed: {
	},
	methods: {
		cancelClicked() {
			this.loginData.Email = "";
			this.loginData.Password = "";
		},
		onClickConfirm() {
			this.$emit("confirmClicked", this.loginData);
		}
	}
});
</script>
<style>

</style>