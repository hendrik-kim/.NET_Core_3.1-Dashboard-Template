<template>
  <div class="add-post container">
    <form @submit.prevent="AddPost">
      <div class="field title">
        <label for="title">제목 :</label>
        <input type="text" name="title" v-model="title" />
      </div>
      <div class="field content">
        <label for="content">의뢰내용 :</label>
        <input type="text" name="content" v-model="content" />
      </div>
      <div id="category" class="field category">
        <label for="category">카테고리 :</label>
        <select
          name="category"
          style="display: block !important;"
          @change="onCategorySelect"
          v-model="selectedOption"
        >
          <option value="-1">선택</option>
          <option
            :value="option.id"
            v-for="option in categoryOptions"
            :key="option.id"
          >{{option.label}}</option>
        </select>
        <!-- <input type="text" name="category" v-model="category.id" /> -->
      </div>
      <div class="field location">
        <label for="location">Location :</label>
        <input class="location-input" type="text" name="location" v-model="location" />
      </div>
      <div v-for="(tagName, index) in tags" :key="index">
        <label for="tagName"></label>
      </div>
      <div class="field add-tagName">
        <label for="add-tagName">태그 :</label>
        <input
          type="text"
          name="add-tagName"
          @keydown.188.prevent="AddTag"
          @keydown.enter.prevent="AddTag"
          @keydown.tab.prevent="AddTag"
          v-model="tagName"
        />
        <div class="container-tags">
          <div class="tagName-chip" v-for="(tag, index) in tags" :key="index">
            <div class="label">{{tag.tagName}}</div>
            <div class="btn-remove" @click="tags.splice(index, 1)">X</div>
          </div>
        </div>
      </div>
      <div id="btn-submit" class="field center-align">
        <p v-if="feedback" class="red-text">{{feedback}}</p>
        <div v-on:click="AddPost" class="btn pink">제출하기</div>
      </div>
    </form>
  </div>
</template>

<script>
export default {
  name: "AddPost",
  data() {
    return {
      categoryOptions: [
        { id: 4, label: "Desktop" },
        { id: 5, label: "Mobile" },
        { id: 6, label: "Web" }
      ],
      title: null,
      content: null,
      tagName: null,
      tags: [],
      selectedOption: -1,
      category: {},
      id: null,
      location: null,
      feedback: null
    };
  },
  created() {
    // console.log(
    //   navigator.geolocation.getCurrentPosition(console.log, console.error)
    // );
  },
  methods: {
    onCategorySelect(e) {
      console.log(e, this.selectedOption);
      let selected = this.categoryOptions.filter(
        el => el.id == this.selectedOption
      );
      if (selected) {
        this.category = selected[0];
      } else {
        this.category = {};
      }
      console.log(this.category);
    },
    AddPost() {
      // this.category.id = 4;
      if (this.title) {
        this.feedback = null;
        this.$axios
          .post(
            "https://localhost:44371/api/post",
            {
              title: this.title,
              content: this.content,
              category: this.category,
              location: this.location,
              tags: this.tags
            },
            { "content-type": "text/json" }
          )
          .then(() => {
            this.$router.push({ name: "Index" }).catch(err => {
              consoe.log(error.response);
            });
          });
      } else {
        this.feedback = "제목을 입력해 주십시오.";
      }
      console.log(this.title, this.content, this.tags);
    },
    AddTag() {
      if (this.tags.length < 5) {
        if (this.tagName) {
          var tag = { tagName: null };
          tag.tagName = this.tagName;
          this.tags.push(tag);
          // this.tags = this.tags.filter(
          //   (el, index, arr) => arr.indexOf(el) === index
          //   // this.feedback = "d";
          // );
          // arr = [1,1,2,3,1]
          // index : [0,1,2,3,4]
          // arr.indexOf(el): [0,0,2,3,0]
          this.tagName = null;
          this.feedback = null;
        } else {
          this.feedback = "태그를 입력하세요.";
        }
      } else {
        this.feedback = "태그는 5개까지 입력이 가능합니다.";
        this.tagName = null;
      }
    }
    // AddLocation() {}
  }
};
</script>

<style>
.container {
  background: #fff;
}

.add-post {
  margin-top: 60px;
  padding: 20px;
  max-width: 500px;
}
.tagName-chip {
  display: inline-flex;
  align-items: center;
  justify-content: center;
  margin-right: 10px;
  margin-bottom: 10px;
  padding: 5px;
  background-color: #dfdfdf;
  border-radius: 20px;
}
.label {
  padding: 0px 5px;
}
.btn-remove {
  cursor: pointer;
  padding: 5px;
  border-radius: 50%;
  width: 20px;
  height: 20px;
  box-sizing: border-box;
  background-color: #585858;
  color: #fff;
  display: inline-flex;
  align-items: center;
  justify-content: center;
}

#category {
  margin-bottom: 10px;
}

#btn-submit {
  margin-top: 20px;
  margin-bottom: 30px;
}
</style>