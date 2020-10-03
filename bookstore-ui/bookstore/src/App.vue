<template>
  <div id="app">
    <br>
    <br>
    <div class="container">
      <br>
      <h3>{{title}}</h3>
      <br>
      <form id="book-form">
        <div class="col-md-12">
          <div class="row">
            <div class="form-group col-md-6">
              <label>Kitap İsmi</label>
              <input type="text" class="form-control" v-model="booksDto.name">
            </div>
            <div class="form-group col-md-6">
              <label>Sayfa Sayısı</label>
              <input type="text" class="form-control" v-model="booksDto.numberOfPage">

            </div>
          </div>
          <div class="row">
            <div class="form-group col-md-6">
              <label>Yazar Seçiniz</label>
              <select class="form-control" v-model="booksDto.authorId">
                <option value="1">Author 1</option>
                <option value="2">Author 2</option>
                <option value="3">Author 3</option>
                <option value="4">Author 4</option>
                <option value="5">Author 5</option>
              </select>
            </div>
            <div class="form-group col-md-6">
              <label>Yayın Evi Seçiniz</label>
              <select class="form-control" v-model="booksDto.publisherId">
                <option value="1">Publisher 1</option>
                <option value="2">Publisher 2</option>
                <option value="3">Publisher 3</option>
                <option value="4">Publisher 4</option>
                <option value="5">Publisher 5</option>
              </select>
            </div>
          </div>
          <div class="row">
            <div class="form-group col-md-12">
              <label>Kategori Seçiniz</label>
              <select class="form-control" v-model="booksDto.categoryId">
                <option value="1">Category 1</option>
                <option value="2">Category 2</option>
                <option value="3">Category 3</option>
                <option value="4">Category 4</option>
                <option value="5">Category 5</option>
              </select>
            </div>
            <div class="form-group col-md-6">
              <button type="button" v-if="!buttonType" @click="addBook" class="btn btn-success">Kaydet</button>
              <button type="button" v-if="buttonType" @click="updateBook" class="btn btn-primary">Güncelle</button>
              <button type="button" v-if="buttonType" @click="clear" class="btn btn-dark">İptal</button>
            </div>
          </div>
        </div>
      </form>
      <table class="table">
        <thead>
        <tr>
          <th scope="col">Kitap İsmi</th>
          <th scope="col">Sayfa Sayısı</th>
          <th scope="col">Kategori</th>
          <th scope="col">Yazar</th>
          <th scope="col">Yayın Evi</th>
          <th scope="col">Sil</th>
          <th scope="col">Güncelle</th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="(book,index) in bookList">
          <td>{{book.name}}</td>
          <td>{{book.numberOfPage}}</td>
          <td>{{book.categoryName}}</td>
          <td>{{book.authorName}}</td>
          <td>{{book.publisherName}}</td>
          <td>
            <button @click="deleteBook(book.id,index)" class="btn-danger btn-xs">Sil</button>
          </td>
          <td>
            <button @click="getBook(book.id)" class="btn-danger btn-xs">Güncelle</button>
          </td>
        </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
export default {
  name: 'app',
  data() {
    return {
      booksDto: {
        id: 0,
        name: null,
        numberOfPage: 0,
        categoryId: 0,
        authorId: 0,
        publisherId: 0
      },
      bookList: {
        id: 0,
        name: null,
        numberOfPage: null,
        categoryName: null,
        authorName: null,
        publisherName: null
      },
      buttonType: false,
      title: "Kitap Girişi",
      divShowHide:true
    }
  },
  methods: {
    addBook() {
      const self=this;
      this.$http.post("add-book", self.booksDto, {headers: {'Content-Type': 'application/json'}}).then(function(response) {
        alert(response.body.entity);
        if(response.body.isSuccessful == true){
           
          this.getAllBooks();
          this.clear();
        }
        else
          alert("Ekleme işlemi başarısız");
      });
    },
    deleteBook(id, index) {
      this.$http.delete("delete-book/" + id).then(function(response) {
        if(response.body.isSuccessful == true){
          this.bookList.splice(index, 1);
          this.getAllBooks();
        }
        else
          alert("Silme işlemi başarısız");
      });
    },
    getBook(id) {
      this.$http.get("get-book/" + id).then(function(response) {
        this.booksDto=response.body.entity;
        this.buttonType=true
        this.title="Kitap Düzenle";
      });
    },
    updateBook() {
      const self=this;
      this.$http.put("update-book", self.booksDto).then(function(response) {
        if(response.body.isSuccessful == true){
          this.buttonType=false
          this.clear();
          this.getAllBooks();
        }
        else
          alert("Düzenleme işlemi başarısız");
      });
    },
    getAllBooks() {
      this.$http.get("get-all-book").then(function(response) {
        this.bookList=response.body.entity;
      });
    },
    clear() {
      this.buttonType=false;
      Object.keys(this.booksDto).forEach(key => {
        this.booksDto[key]=''
      });
      this.title="Kitap Girişi";
    },
  },
  mounted() {
    this.getAllBooks();
  },
}
</script>
<style scoped>
.container {
  border: solid 2px;
  box-shadow: 4px 4px 4px 4px #000;
}
</style>
