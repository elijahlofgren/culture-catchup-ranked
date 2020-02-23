<template>
  <div class="home">
    <img src="../assets/images/film-4208954_640.jpg">
    <MovieListing />
  </div>
</template>

<script>
// @ is an alias to /src
import MovieListing from '@/components/MovieListing.vue'

export default {
  name: 'home',
  components: {
    MovieListing
  }
}
</script>
