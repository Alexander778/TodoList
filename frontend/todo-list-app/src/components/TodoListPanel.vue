<template>
    <div>
        <AddEditCategory :openPopup.sync="popupOpen" @close="hidePopup" :editMode="isEditMode"
            :categoryToEdit="categories.find(i => i.id == selectedCategory)" :activeUserId="selectedUserId" />
        <vs-row vs-justify=" center">
            <vs-col type="flex" vs-justify="center" vs-align="center" vs-w="6">
                <vs-card>
                    <div slot="header">
                        <h3>
                            Todo list
                        </h3>
                    </div>
                    <div v-if="categories.length > 0">
                        <vs-select v-model="selectedCategory">
                            <vs-select-item :key="index" :value="category.id" :text="category.name"
                                v-for="(category, index) in categories" />
                        </vs-select>

                        <vs-button color="primary" icon="edit" :disabled="selectedCategory == null"
                            @click="showEditPopup">
                        </vs-button>
                        <div v-if="selectedCategoryObject.todoTasks.length > 0">
                            <vs-list>
                                <TodoTask :todoTask="todoTaskItem" :key="index"
                                    v-for="(todoTaskItem, index) in selectedCategoryObject.todoTasks" />
                            </vs-list>
                        </div>
                    </div>
                    <div v-else>
                        <h3>You haven't had any categories yet.</h3>
                    </div>
                    <vs-button color="primary" icon="add" @click="showPopup"></vs-button>
                </vs-card>
            </vs-col>
        </vs-row>
    </div>
</template>

<script>
import todoListService from '../../services/todoListService';
import AddEditCategory from '../popups/AddEditCategory.vue';
import TodoTask from './TodoTask.vue';

export default {
    name: 'TodoListPanel',
    components: { AddEditCategory, TodoTask },
    props: {
        selectedUserId: Number
    },
    data: () => ({
        selectedCategory: null,
        selectedCategoryObject: null,
        categories: [],
        popupOpen: false,
        isEditMode: false
    }),
    async beforeMount() {
        if (this.selectedUserId !== null) {
            await this.getCategories(this.selectedUserId);
        }
    },
    methods: {
        async getCategories(userId) {
            this.categories = await todoListService.getCategories(userId);
            if (this.categories.length != 0) {
                this.selectedCategory = this.categories[0].id;
                this.selectedCategoryObject = this.categories[0];
            } else {
                this.categories = [];
                this.selectedCategory = null;
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
        }
    },
    watch: {
        async selectedUserId(value) {
            await this.getCategories(value);
        },
        selectedCategory(value) {
            this.selectedCategoryObject = this.categories.find(i => i.id === value);
        }
    }
}
</script>