mergeInto(LibraryManager.library, {

  GetDataVK: function() {
    getUserData();
  },
  
  SetData : function(data){
    setUserData(UTF8ToString(data));
  },

  VKShowAdvExtern: function() {
    showFullscrenAd();
  },

  VKRewardAdvExtern: function() {
    showRewardedAd();
  }, 

  VKRewardNextLevelAdvExtern: function() {
    showRewardNextLevelAd();
  }, 

  GoToGroupExtern: function(){
    goToGroup();
  },

  VKFriendExtern: function(){
    goToFriend();
  }
});