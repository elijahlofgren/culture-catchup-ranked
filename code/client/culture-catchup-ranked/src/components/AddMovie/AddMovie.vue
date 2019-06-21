<script src="./AddMovie.js" />
<template>
  <div class="add-movie">
    <h2>Add New Movie</h2>
    <v-text-field label="Movie Title" v-model="title"></v-text-field>
    <v-btn @click="addMovie">Add</v-btn>
  </div>
</template>