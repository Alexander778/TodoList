import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        selectedUser: null
    },
    getters: {},
    mutations: {
        changeSelectedUser(state, payload) {
            state.selectedUser = payload;
        }
    },
    actions: {}
});