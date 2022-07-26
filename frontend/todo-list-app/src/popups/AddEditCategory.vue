<template>
    <div class="centerx">
        <vs-popup class="holamundo" title="Add user" :active.sync="active">
            <vs-row>
                <vs-col vs-type="flex-start" vs-justify="center" vs-align="center" vs-w="12">
                    <vs-input placeholder="Name" v-model="category.name" />
                </vs-col>
            </vs-row>
            <vs-row>
                <vs-button color="primary" :disabled="validToSubmit" @click="saveCategory">Save</vs-button>
            </vs-row>
        </vs-popup>
    </div>
</template>

<script>
import todoListService from '../../services/todoListService';


export default {
    name: 'AddEditCategory',
    components: {},
    props: {
        openPopup: Boolean,
        editMode: Boolean,
        categoryToEdit: Object
    },
    data: () => ({
        category: {
            id: 0,
            name: "",
            userId: 1
        }
    }),
    watch: {
        active(newVal) {
            if (newVal && this.editMode && this.categoryToEdit) {
                this.category.name = this.categoryToEdit.name;
                this.category.id = this.categoryToEdit.id;
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
            return this.category.name === '';
        }
    },
    methods: {
        resetForm() {
            this.category.name = "";
        },
        async saveCategory() {
            let response = "";
            if (this.editMode) {
                response = await todoListService.updateCategory(this.category);
            } else {
                response = await todoListService.addCategory(this.category);
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
