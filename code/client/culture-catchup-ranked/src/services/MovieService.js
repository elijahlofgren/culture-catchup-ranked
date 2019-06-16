import axios from 'axios';

export default {
  getMovies() {
    return axios(process.env.VUE_APP_API_URL + 'Movie');
  },
  upVote(movieId) {
    return axios.post(process.env.VUE_APP_API_URL + 'Movie/UpVote/' + movieId, {});
  },
  downVote(movieId) {
    return axios.post(process.env.VUE_APP_API_URL + 'Movie/DownVote/' + movieId, {});
  }
}