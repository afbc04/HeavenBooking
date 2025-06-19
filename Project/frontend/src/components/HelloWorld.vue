<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    
    <div v-if="loading">Carregando dados...</div>
    <div v-else-if="error">Erro ao carregar dados: {{ error }}</div>
    <div v-else>
      <h3>Dados da API</h3>
      <ul>
        <li v-for="item in items" :key="item.id">
          {{ item.title }}
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
export default {
  name: 'HelloWorld',
  props: {
    msg: String
  },
  data() {
    return {
      items: [],
      loading: false,
      error: null
    }
  },
  mounted() {
    this.fetchData();
  },
  methods: {
    async fetchData() {
      this.loading = true;
      this.error = null;
      try {
        const res = await fetch('http://localhost:25000/');
        if (!res.ok) throw new Error(`HTTP error! status: ${res.status}`);
        const data = await res.json();
        this.items = data; // assume que data é um array
      } catch (err) {
        this.error = err.message;
      } finally {
        this.loading = false;
      }
    }
  }
}
</script>

<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  margin: 0 0 10px;
}
a {
  color: #42b983;
}
</style>
