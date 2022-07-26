<template>
  <div>
    <UserPanel />
  </div>
</template>

<script>
import UserPanel from './components/UserPanel.vue';

export default {
  name: 'App',
  components: { UserPanel }
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
