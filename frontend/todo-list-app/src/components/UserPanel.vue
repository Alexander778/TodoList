<template>
    <div>
        <AddEditUser :openPopup.sync="popupOpen" @close="hidePopup" :editMode="isEditMode"
            :userToEdit="users.find(i => i.id == selectedUser)" />
        <vs-row vs-justify="center">
            <vs-col type="flex" vs-justify="center" vs-align="center" vs-w="6">
                <vs-card>
                    <div slot="header">
                        <h3>
                            Select user
                        </h3>
                    </div>
                    <div>
                        <vs-select class="selectExample" v-model="selectedUser">
                            <vs-select-item :key="index" :value="user.id" :text="user.name + ' ' + user.surname"
                                v-for="(user, index) in users" />
                        </vs-select>
                        <vs-button color="primary" icon="add" @click="showPopup"></vs-button>
                        <vs-button color="primary" icon="edit" :disabled="selectedUser == null" @click="showEditPopup">
                        </vs-button>
                    </div>
                </vs-card>
            </vs-col>
        </vs-row>
    </div>
</template>

<script>
import todoListService from '../../services/todoListService';
import AddEditUser from '@/popups/AddEditUser.vue';

export default {
    name: 'UserPanel',
    components: { AddEditUser },
    data: () => ({
        selectedUser: null,
        users: [],
        popupOpen: false,
        isEditMode: false
    }),
    async beforeMount() {
        await this.getUsers();
    },
    methods: {
        async getUsers() {
            this.users = await todoListService.getUsers();
            if (this.users.length != 0) {
                this.selectedUser = this.users[0].id;
            }
        },
        showPopup() {
            this.popupOpen = true;
            this.isEditMode = false;
        },
        async hidePopup() {
            this.popupOpen = false;
            await this.getUsers();
        },
        showEditPopup() {
            this.popupOpen = true;
            this.isEditMode = true;
        }
    }
}
</script>