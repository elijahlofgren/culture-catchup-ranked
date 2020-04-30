<script src="./MovieVoteCards.js" />
<template>
  <v-card max-width="400" class="mx-auto">
    <v-system-bar color="pink darken-2" dark>
      <v-spacer></v-spacer>

      <v-icon>mdi-window-minimize</v-icon>

      <v-icon>mdi-window-maximize</v-icon>

      <v-icon>mdi-close</v-icon>
    </v-system-bar>

    <v-app-bar dark color="pink">
      <v-app-bar-nav-icon></v-app-bar-nav-icon>

      <v-toolbar-title>Movies</v-toolbar-title>
      <!--<p>Swipe right to upvote. Swipe left to downvote</p>-->
      <v-spacer></v-spacer>

      <v-btn icon>
        <v-icon>mdi-magnify</v-icon>
      </v-btn>
    </v-app-bar>

    <v-container>
      <div v-if="loading">Loading...</div>
      <v-row v-else>

        <v-col v-for="(item, i) in movies" :key="i" cols="12">
          <v-card dark>
            <div class="movie-card d-flex flex-no-wrap justify-space-between">
              <!-- grids for Vuetify 1.5 https://v15.vuetifyjs.com/en/framework/grid -->
           <v-layout row wrap>

            <v-flex xs6 sm6 md6>
                <v-card-title class="headline" v-text="item.movie.title"></v-card-title>
                <p v-if="item.movieInfo.Director">{{item.movieInfo.Director}} ({{item.movieInfo.Year}})</p>
                <v-card-subtitle v-if="item.movieInfo.plot" class="sub-title" v-text="item.movieInfo.plot"></v-card-subtitle>
             </v-flex>
               <v-flex v-if="!hasVoted(item)" xs6 sm6 md6>
                <v-btn @click="upVote(item)" icon><v-icon>fa-thumbs-up<v-icon> Upvote</v-btn>
              <v-btn @click="upVote(item)" icon><v-icon>fa-thumbs-down<v-icon>Downvote</v-btn>
               <v-avatar v-if="item.movieInfo.poster && item.movieInfo.poster.length > 0" class="ma-3" size="125" tile>
                <v-img :src="item.movieInfo.poster"></v-img>
              </v-avatar>
        </v-flex>
            </v-layout>
            </div>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </v-card>
</template>