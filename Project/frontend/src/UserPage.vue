<template>
  <div v-if="user === null && !loading && !error">
    <p>Utilizador não existe</p>
  </div>
  <div v-else-if="loading">
    <p>A carregar...</p>
  </div>
  <div v-else-if="error">
    <p>Erro: {{ error }}</p>
  </div>
  <div v-else>
    <ul>
      <li><b>ID :</b> {{ user.id }}</li>
      <li><b>Nome :</b> {{ user.name }}</li>
      <li><b>Idade :</b> {{ user.age }}</li>
      <li><b>Codigo de País :</b> {{ user.country_code }}</li>
      <li><b>Passaporte :</b> {{ user.passport }}</li>
      <li><b>Reservas :</b> {{ user.reservations }}</li>
      <li><b>Reservas Reembolsadas :</b> {{ user.reservations_refunded }}</li>
      <li><b>Voos :</b> {{ user.flights }}</li>
      <li><b>Voos Embarcados :</b> {{ user.flights_arrived }}</li>
      <li><b>Voos Perdidos :</b> {{ user.flights_lost }}</li>
      <li><b>Gasto Total :</b> {{ user.total_spent }}</li>
      <li><b>Está Ativo? :</b> {{ user.active }}</li>
    </ul>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  name: 'UserPage',
  data() {
    return {
      user: null,
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
        const userId = this.$route.params.id;
        console.log("USER ID:", userId);

        const res = await axios.get(`/api/users/${userId}`);
        this.user = res.data;
      } catch (err) {
        console.error(err);
        this.error = err.response ? err.response.statusText : err.message;
        this.user = null;
      } finally {
        this.loading = false;
      }
    }
  },
  watch: {
    '$route.params.id': 'fetchData'  // para recarregar ao mudar o ID na rota
  }
}
</script>

<style scoped>
ul {
  list-style-type: none;
  padding: 0;
}
li {
  margin-bottom: 10px;
}
</style>
