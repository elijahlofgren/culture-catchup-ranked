import MovieService from "../services/MovieService.js";

import AddMovie from './AddMovie/AddMovie.vue';

export default {
  name: "MovieListing",
  components: {
    AddMovie
  },
  props: {
    msg: String
  },
  data: function () {
    return {
      search: "",
      headers: [
        {
          text: "Vote Score",
          width: 50,
          sortable: true,
          value: "voteSum"
        },
        {
          text: "Upvotes",
          width: 50,
          sortable: true,
          value: "upVoteCount"

        },
        {
          text: "Downvotes",
          width: 50,
          sortable: true,
          value: "downVoteCount"

        },
        {
          text: "Actions",
          width: 290,
          sortable: false
        },
        {
          text: "Movie Name",
          sortable: true,
          value: "movieTitle"
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

      MovieService.getMoviesWithVotes()
        .then((response) => {
          console.log("getMoviesWithVotes response = ");
          console.log(JSON.stringify(response));
          console.log(response);
          response.json().then((data) => {
            let movies = data;
            vm.movies = movies;
          });

        })
        .catch(error => {
          console.error('Error =');
          console.error(error);
          window.location.href = '/Identity/Account/Login';

        });
    }
  }
};