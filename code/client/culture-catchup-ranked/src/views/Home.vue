<template>
  <div class="home">
    <!--<img src="../assets/images/film-4208954_640.jpg">-->
    <MovieVoteCards />
  </div>
</template>

<script>
// @ is an alias to /src
import MovieVoteCards from '@/components/MovieVoteCards/MovieVoteCards.vue'

export default {
  name: 'home',
  components: {
    MovieVoteCards
  }
}
</script>
