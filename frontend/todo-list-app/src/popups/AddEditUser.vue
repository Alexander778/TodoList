<template>
    <div class="centerx">
        <vs-popup :title="editMode ? `Edit user` : `Add user`" :active.sync="active">
            <vs-row>
                <vs-col class="flex justify-center mb-2">
                    <vs-input placeholder="Name" v-model="user.name" />
                </vs-col>
            </vs-row>
            <vs-row>
                <vs-col class="flex justify-center mb-2">
                    <vs-input placeholder="Surname" v-model="user.surname" />
                </vs-col>
            </vs-row>
            <vs-row class="flex justify-center">
                <vs-button color="primary" :disabled="validToSubmit" @click="saveNewUser">Save</vs-button>
            </vs-row>
        </vs-popup>
    </div>
</template>
<script>
import todoListService from '../../services/todoListService';


export default {
    name: 'AddEditUser',
    components: {},
    props: {
        openPopup: Boolean,
        editMode: Boolean,
        userToEdit: Object
    },
    data: () => ({
        user: {
            id: "",
            name: "",
            surname: ""
        }
    }),
    watch: {
        active(newVal) {
            if (newVal && this.editMode && this.userToEdit) {
                this.user.name = this.userToEdit.name;
                this.user.surname = this.userToEdit.surname;
                this.user.id = this.userToEdit.id;
            }
        }
    },
    computed: {
        active: {
            get() {
                return this.openPopup;
            },
            set() {
                this.$emit('close', false);
                this.resetForm();
            }
        },
        validToSubmit() {
            return this.user.name === ''
                || this.user.surname === '';
        }
    },
    methods: {
        resetForm() {
            this.user.name = "";
            this.user.surname = "";
        },
        async saveNewUser() {
            let response = "";
            if (this.editMode) {
                response = await todoListService.updateUser(this.user);
            } else {
                response = await todoListService.addUser(this.user);
            }
            if (response == 1) {
                this.resetForm();
                this.$emit('close', false);
            }
            // TODO: add loader, success/error notification
        }
    }
}
</script>
<style>
.vs-popup {
    width: 300px !important;
}
</style>
