<template>
  <div class="home">
    <img src="../assets/images/film-4208954_640.jpg">
    <MyUpVotes />
  </div>
</template>

<script>
// @ is an alias to /src
import MyUpVotes from '@/components/MyUpVotes/MyUpVotes.vue'

export default {
  name: 'home',
  components: {
    MyUpVotes
  }
}
</script>
