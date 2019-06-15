import axios from 'axios';

export default {
  getMovies() {
    return axios(process.env.VUE_APP_API_URL + 'Movie');
  }
}