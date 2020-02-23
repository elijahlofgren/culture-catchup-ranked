<template>
  <div class="home">
    <img src="../assets/images/film-4208954_640.jpg">
    <MovieTable />
  </div>
</template>

<script>
// @ is an alias to /src
import MovieTable from '@/components/MovieTable/MovieTable.vue'

export default {
  name: 'home',
  components: {
    MovieTable
  }
}
</script>
