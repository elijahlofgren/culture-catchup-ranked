<script src="./AddMovie.js" />
<template>
  <div class="add-movie">
    <h2>Search for new movie to add</h2>
    <!--<v-text-field label="Movie Title" v-model="title"></v-text-field>
    <v-btn @click="addMovie">Add</v-btn>-->

    <v-text-field label="Movie Title" v-model="title"></v-text-field>
    <v-btn @click="searchForMovie">Search</v-btn>

    <div v-if="searchResults">
      <v-col v-for="(item, i) in searchResults" :key="i" cols="12">
        <v-card dark>
          <div class="movie-card d-flex flex-no-wrap justify-space-between">
            <!-- grids for Vuetify 1.5 https://v15.vuetifyjs.com/en/framework/grid -->
            <v-layout row wrap>
              <v-flex xs12>
                <v-btn @click="addMovieFromSearch(item)">+ Add</v-btn>
                <v-card-title class="headline" v-text="item.title"></v-card-title>
                <p
                  v-if="item.director"
                >{{item.director}} ({{item.year}})</p>
                <div v-if="item.plot" class="plot" v-text="item.plot"></div>
              </v-flex>
              <v-flex xs12>
             
                <a target="_blank" title="View IMDB" :href="'https://www.imdb.com/title/' + item.imdbID">
                  <v-avatar
                    v-if="item.poster && item.poster.length > 0"
                    class="ma-3"
                    size="125"
                    tile
                  >
                    <v-img :src="item.poster"></v-img>
                  </v-avatar>
                </a>
              </v-flex>
            </v-layout>
          </div>
        </v-card>
      </v-col>
    </div>
  </div>
</template>