import './AddMovie.scss';
import MovieService from "../../services/MovieService.js";

export default {
  props: {
    msg: String
  },
  data: function () {
    return {
     //title: '',
     searchResults: null
    };
  },
  created() {
    let vm = this;
  },
  methods: {
    searchForMovie() {
      let vm = this;
      MovieService.searchForMovie(vm.title)
      .then((response) => {
        console.log("Search results returned");
        response.json().then((data) => {
          vm.searchResults = data;
        });
        
      })
      .catch(error => {
        console.error(error);
      });
    },
    addMovieFromSearch(item) {
      let vm = this;
      MovieService.addMovie({title: item.title, imdbID: item.imdbID})
      .then(() => {
        console.log("Movie Added");
      })
      .catch(error => {
        console.error(error);
      });
    }
    /*
    addMovie() {
      let vm = this;
      MovieService.addMovie({title: vm.title})
      .then(() => {
        console.log("Movie Added");
      })
      .catch(error => {
        console.error(error);
      });
    }*/
  }
};