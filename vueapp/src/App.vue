<template>
  <EventList />
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import EventList from './components/EventList.vue';

export default defineComponent({
  name: 'App',
  components: {
    EventList
  }
});
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
