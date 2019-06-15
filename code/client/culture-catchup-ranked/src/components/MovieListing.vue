<template>
  <div class="hello">
    <h1>Culture Catchup Movie List</h1>
    <div v-if="movies">
    <v-data-table :headers="headers" :items="movies" class="elevation-1">
      <template v-slot:items="props">
        <td>{{ props.item.title }}</td>
        <td class="text-xs-right"><v-btn>Upvote</v-btn> <v-btn>Downvote</v-btn></td>
      </template>
    </v-data-table>
    </div>
  </div>
</template>

<script>
import MovieService from "../services/MovieService.js";

export default {
  name: "HelloWorld",
  props: {
    msg: String
  },
  data: function() {
    return {
      headers: [
        {
          text: "Movie Name",
          align: "left",
          sortable: true,
          value: "title"
        },
      ],
      movies: null
    };
  },
  created() {
    let vm = this;
    vm.getMovies();
  },
  methods: {
    getMovies() {
      let vm = this;
      MovieService.getMovies()
        .then(response => {
          let movies = response.data;
          console.log("movies = ");
          console.log(JSON.stringify(movies));
          vm.movies = movies;
        })
        .catch(error => {
          console.error(error);
        });
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
