<template>
    <div>
        <AddEditCategory :openPopup.sync="popupOpen" @close="hidePopup" :editMode="isEditMode"
            :categoryToEdit="categories.find(i => i.id == selectedCategory)" />
        <vs-row vs-justify=" center">
            <vs-col type="flex" vs-justify="center" vs-align="center" vs-w="6">
                <vs-card>
                    <div slot="header">
                        <h3>
                            Todo list
                        </h3>
                    </div>
                    <div>
                        <vs-select v-model="selectedCategory">
                            <vs-select-item :key="index" :value="category.id" :text="category.name"
                                v-for="(category, index) in categories" />
                        </vs-select>

                        <vs-button color="primary" icon="add" @click="showPopup"></vs-button>
                        <vs-button color="primary" icon="edit" :disabled="selectedCategory == null"
                            @click="showEditPopup">
                        </vs-button>
                    </div>
                </vs-card>
            </vs-col>
        </vs-row>
    </div>
</template>

<script>
import todoListService from '../../services/todoListService';
import AddEditCategory from '../popups/AddEditCategory.vue';

export default {
    name: 'TodoListPanel',
    components: { AddEditCategory },
    data: () => ({

        selectedCategory: null,
        categories: [],
        popupOpen: false,
        isEditMode: false
    }),
    async beforeMount() {
        await this.getCategories();
    },
    methods: {
        async getCategories() {
            this.categories = await todoListService.getCategories(1); // TODO pass selected user id
            if (this.categories.length != 0) {
                console.log(this.categories[0].id);
                this.selectedCategory = this.categories[0].id;
            }
        },
        showPopup() {
            this.popupOpen = true;
            this.isEditMode = false;
        },
        async hidePopup() {
            this.popupOpen = false;
            await this.getCategories();
        },
        showEditPopup() {
            this.popupOpen = true;
            this.isEditMode = true;
        }
    }
}
</script>