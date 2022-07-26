<template>
    <div class="centerx">
        <vs-popup class="holamundo" title="Add user" :active.sync="active">
            <vs-row>
                <vs-col vs-type="flex-start" vs-justify="center" vs-align="center" vs-w="12">
                    <vs-input placeholder="Name" v-model="user.name" />
                </vs-col>
            </vs-row>
            <vs-row>
                <vs-col vs-type="flex-start" vs-justify="center" vs-align="center" vs-w="12">
                    <vs-input placeholder="Surname" v-model="user.surname" />
                </vs-col>
            </vs-row>
            <vs-row>
                <vs-button color="primary" :disabled="validToSubmit">Save</vs-button>
            </vs-row>
        </vs-popup>
    </div>
</template>
<script>

export default {
    name: 'AddEditUser',
    components: {},
    props: {
        openPopup: Boolean
    },
    data: () => ({
        user: {
            name: "",
            surname: ""
        }
    }),
    computed: {
        active: {
            get() {
                return this.openPopup;
            },
            set() {
                this.$emit('close', false);
                this.user.name = "";
                this.user.surname = "";
            }
        },
        validToSubmit() {
            console.log(this.user);
            return this.user.name === '' || this.user.surname === '';
        }
    },
    methods: {}
}
</script>
<style>
.vs-popup {
    width: 300px !important;
}
</style>
