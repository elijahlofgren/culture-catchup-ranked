import MovieService from "../../services/MovieService.js";

export default {
  name: "Ranking",
  components: {
    
  },
  props: {
    msg: String
  },
  data: function () {
    return {
      search: "",
      headers: [
        {
          text: "Votes",
          width: 290,
          sortable: true
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
    vm.getMoviesWithVotes();
  },
  methods: {
    searchImdb(movieItem) {
      window.open(
        "https://www.imdb.com/find?ref_=nv_sr_fn&q=" +
        encodeURI(movieItem.title) +
        "&s=tt"
      );
    },
    getMoviesWithVotes() {
      let vm = this;

      MovieService.getMoviesWithVotes()
        .then((response) => {
          console.log("response = ");
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