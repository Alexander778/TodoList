<template>
  <div>
    <UserPanel @updateUserId="setupSelectedUser" />
    <TodoListPanel :selectedUserId="selectedUserId" />
  </div>
</template>

<script>
import UserPanel from './components/UserPanel.vue';
import TodoListPanel from './components/TodoListPanel.vue';

export default {
  name: 'App',
  components: { UserPanel, TodoListPanel },
  data: () => ({
    selectedUserId: null,
  }),
  methods: {
    setupSelectedUser(value) {
      this.selectedUserId = value;
    }
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
