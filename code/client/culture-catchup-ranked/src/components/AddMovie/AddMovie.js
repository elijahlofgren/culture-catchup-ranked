import './AddMovie.scss';
import MovieService from "../../services/MovieService.js";

export default {
  props: {
    msg: String
  },
  data: function () {
    return {
     title: ''
    };
  },
  created() {
    let vm = this;
  },
  methods: {
    addMovie() {
      let vm = this;
      MovieService.addMovie({title: vm.title})
      .then(() => {
        console.log("Down Voted");
      })
      .catch(error => {
        console.error(error);
      });
    }
  }
};