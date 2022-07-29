<template>
    <div class="centerx">
        <vs-popup :title="editMode ? `Edit task` : `Add task`" :active.sync="active">
            <vs-row>
                <vs-col class="flex justify-center">
                    <div v-if="categories.length > 0">
                        <vs-select v-model="task.categoryId">
                            <vs-select-item :key="index" :value="category.id" :text="category.name"
                                v-for="(category, index) in categories" />
                        </vs-select>
                    </div>
                </vs-col>
                <vs-col class="flex justify-center mt-1">
                    <vs-input placeholder="Text" v-model="task.text" />
                </vs-col>
            </vs-row>
            <vs-row class="flex justify-center mt-2">
                <vs-button color="primary" :disabled="validToSubmit" @click="saveTask">Save</vs-button>
            </vs-row>
        </vs-popup>
    </div>
</template>

<script>
import todoListService from '../../services/todoListService';


export default {
    name: 'AddEditTodoTask',
    components: {},
    props: {
        openPopup: Boolean,
        editMode: Boolean,
        taskToEdit: Object,
        categories: Array
    },
    data: () => ({
        task: {
            id: 0,
            categoryId: 0,
            text: "",
            statusId: 1
        }
    }),
    watch: {
        active(newVal) {
            if (newVal && this.editMode && this.taskToEdit) {
                this.task.id = this.taskToEdit.id;
                this.task.categoryId = this.taskToEdit.categoryId;
                this.task.text = this.taskToEdit.text;
                this.task.statusId = this.taskToEdit.statusId;
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
            return this.task.text === "" || this.task.categoryId === 0;
        }
    },
    methods: {
        resetForm() {
            this.task.text = "";
            this.task.categoryId = 0;
        },
        async saveTask() {
            let response = "";
            if (this.editMode) {
                response = await todoListService.updateTask(this.task);
            } else {
                response = await todoListService.addTask(this.task);
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
