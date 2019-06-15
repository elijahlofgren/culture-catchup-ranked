<template>
  <div class="hello">
    <h1>Culture Catchup Movie List</h1>
    <div v-if="movies">
      <v-card>
        <v-card-title>
          <v-text-field
            v-model="search"
            append-icon="search"
            label="Search"
            single-line
            hide-details
          ></v-text-field>
        </v-card-title>
        <v-data-table
          :pagination.sync="pagination"
          :rows-per-page-items="rowsPerPageItems"
          :headers="headers"
          :items="movies"
          :search="search"
        >
          <template v-slot:items="props">
            <td class="text-xs-left">
              <v-btn>Upvote</v-btn>
              <v-btn>Downvote</v-btn>
            </td>
            <td class="text-xs-left">{{ props.item.title }}</td>
            <td class="text-xs-left">
              <v-btn @click="searchImdb(props.item)">IMDB</v-btn>
            </td>
          </template>
          <template v-slot:no-results>
            <v-alert
              :value="true"
              color="error"
              icon="warning"
            >Your search for "{{ search }}" found no results.</v-alert>
          </template>
        </v-data-table>
      </v-card>
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
      search: "",
      headers: [
        {
          text: "Actions",
          width: 290,
          sortable: false
        },
        {
          text: "Movie Name",
          sortable: true,
          value: "title"
        },
        {
          text: "IMDB",
          sortable: false
        }
      ],
      movies: null,
      rowsPerPageItems: [10, 20, 30, 40, 2000],
      pagination: {
        rowsPerPage: 2000
      }
    };
  },
  created() {
    let vm = this;
    vm.getMovies();
  },
  methods: {
    searchImdb(movieItem) {
      window.open(
        "https://www.imdb.com/find?ref_=nv_sr_fn&q=" +
          encodeURI(movieItem.title) +
          "&s=tt"
      );
    },
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
