<template>
    <div>
        <AddEditCategory :openPopup.sync="popupOpen" @close="hidePopup" :editMode="isEditMode"
            :categoryToEdit="categories.find(i => i.id == selectedCategory)" :activeUserId="selectedUserId" />
        <AddEditTodoTask :openPopup.sync="addTaskPopupOpen" @close="hideTaskPopup" :editMode="isEditTaskMode"
            :taskToEdit="currentTaskToEdit" :categories="categories" />
        <vs-row vs-justify="center" id="div-with-loading">
            <vs-col type="flex" vs-justify="center" vs-align="center" vs-w="8">
                <vs-card>
                    <div slot="header" class="flex justify-between">
                        <h3>
                            Todo list
                        </h3>
                        <div class="flex">
                            <div class="flex mr-1 mt-2">
                                <label class="mr-1">Only done</label>
                                <vs-switch v-model="showDoneTasks" />
                            </div>
                            <div v-if="categories.length > 0">
                                <vs-select v-model="selectedCategory">
                                    <vs-select-item :key="index" :value="category.id" :text="category.name"
                                        v-for="(category, index) in categories" />
                                </vs-select>
                            </div>
                            <div class="flex ml-1">
                                <vs-dropdown>
                                    <a class="a-icon" href="#">
                                        <vs-icon class="" icon="more_vert"></vs-icon>
                                    </a>
                                    <vs-dropdown-menu>
                                        <vs-dropdown-item @click="showPopup">
                                            Add category
                                        </vs-dropdown-item>
                                        <vs-dropdown-item @click="showEditPopup" v-if="categories.length > 0">
                                            Edit selected category
                                        </vs-dropdown-item>
                                    </vs-dropdown-menu>
                                </vs-dropdown>
                            </div>
                        </div>
                    </div>
                    <div v-if="selectedCategoryObject !== null && selectedCategoryObject.todoTasks.length > 0">
                        <vs-list>
                            <TodoTask :todoTask="todoTaskItem" :key="index"
                                v-for="(todoTaskItem, index) in selectedCategoryObject.todoTasks" @edit="editTask"
                                @updateList="getCategories(selectedUserId, selectedCategory)" />
                        </vs-list>
                    </div>
                    <div slot="footer" class="mt-10">
                        <vs-row vs-justify="flex-end">
                            <vs-button type="gradient" color="success" icon="add" @click="showTaskPopup"></vs-button>
                        </vs-row>
                    </div>
                </vs-card>
            </vs-col>
        </vs-row>
    </div>
</template>

<script>
import todoListService from '../../services/todoListService';
import AddEditCategory from '../popups/AddEditCategory.vue';
import TodoTask from './TodoTask.vue';
import AddEditTodoTask from '@/popups/AddEditTodoTask.vue';

export default {
    name: 'TodoListPanel',
    components: { AddEditCategory, TodoTask, AddEditTodoTask },
    props: {
        selectedUserId: Number
    },
    data: () => ({
        selectedCategory: null,
        selectedCategoryObject: null,
        categories: [],
        popupOpen: false,
        addTaskPopupOpen: false,
        isEditMode: false,
        showDoneTasks: false,
        isEditTaskMode: false,
        currentTaskToEdit: null
    }),
    async beforeMount() {
        if (this.selectedUserId !== null) {
            await this.getCategories(this.selectedUserId, this.selectedCategory);
        }
    },
    methods: {
        async getCategories(userId, categoryId = null, todoTaskStatusId = null) {
            this.categories = await todoListService.getCategories(userId, categoryId, todoTaskStatusId);
            if (this.categories.length != 0) {
                this.selectedCategory = this.selectedCategory === null ? this.categories[0].id : this.selectedCategory;
                this.selectedCategoryObject = this.categories.find(i => i.id == this.selectedCategory);
            } else {
                this.categories = [];
                this.selectedCategory = null;
                this.selectedCategoryObject = null;
            }
        },
        showPopup() {
            this.popupOpen = true;
            this.isEditMode = false;
        },
        async hidePopup() {
            this.popupOpen = false;
            await this.getCategories(this.selectedUserId);
        },
        showEditPopup() {
            this.popupOpen = true;
            this.isEditMode = true;
        },
        showTaskPopup() {
            this.addTaskPopupOpen = true;
            this.isEditTaskMode = false;
        },
        async hideTaskPopup() {
            this.addTaskPopupOpen = false;
            await this.getCategories(this.selectedUserId);
        },
        showTaskEditPopup() {
            this.addTaskPopupOpen = true;
            this.isEditTaskMode = true;
        },
        editTask(task) {
            this.showTaskEditPopup();
            this.currentTaskToEdit = task;
        }
    },
    watch: {
        async selectedUserId(value) {
            await this.getCategories(value, this.selectedCategory);
        },
        async selectedCategory(value) {
            await this.getCategories(this.selectedUserId, value);
        },
        async showDoneTasks(value) {
            let statusId = value ? 2 : null;
            await this.getCategories(this.selectedUserId, this.selectedCategory, statusId);
        }
    }
}
</script>