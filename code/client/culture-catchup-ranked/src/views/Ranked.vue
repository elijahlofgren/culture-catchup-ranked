<template>
  <div class="home">
    <img src="../assets/images/film-4208954_640.jpg">
    <Ranking />
  </div>
</template>

<script>
// @ is an alias to /src
import Ranking from '@/components/Ranking/Ranking.vue'

export default {
  name: 'home',
  components: {
    Ranking
  }
}
</script>
