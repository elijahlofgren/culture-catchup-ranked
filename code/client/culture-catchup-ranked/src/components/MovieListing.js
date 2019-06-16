import MovieService from "../services/MovieService.js";

export default {
  name: "MovieListing",
  props: {
    msg: String
  },
  data: function () {
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
    upVote(movieItem) {
      MovieService.upVote(movieItem.id)
      .then(() => {
        console.log("Up Voted");
      })
      .catch(error => {
        console.error(error);
      });
    },
    downVote(movieItem) {
      MovieService.downVote(movieItem.id)
      .then(() => {
        console.log("Down Voted");
      })
      .catch(error => {
        console.error(error);
      });
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